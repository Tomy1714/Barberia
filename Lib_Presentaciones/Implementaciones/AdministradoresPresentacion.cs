using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class AdministradoresPresentacion : IAdministradoresPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<Administradores> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Administradores?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Administradores>();

            return JsonConvert.DeserializeObject<List<Administradores>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<AdministradoresAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Administradores/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<AdministradoresAuditoria>();

            return JsonConvert.DeserializeObject<List<AdministradoresAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<Administradores> Guardar(Administradores entidad, string rol)
        {
            if (entidad.IdAdministrador != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Administradores?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new Administradores();

            return JsonConvert.DeserializeObject<Administradores>(
                respuesta["Valor"].ToString()!)!;
        }

        public Administradores Modificar(Administradores entidad, string rol)
        {
            if (entidad.IdAdministrador == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Administradores?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Administradores();

            return JsonConvert.DeserializeObject<Administradores>(
                respuesta["Valor"].ToString()!)!;
        }

        public Administradores Eliminar(Administradores entidad, string rol)
        {
            if (entidad.IdAdministrador == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Administradores?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Administradores();

            return JsonConvert.DeserializeObject<Administradores>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
