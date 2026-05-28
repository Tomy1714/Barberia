using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class BarberosPresentacion : IBarberosPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<Barberos> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Barberos?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Barberos>();

            return JsonConvert.DeserializeObject<List<Barberos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<BarberosAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Barberos/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<BarberosAuditoria>();

            return JsonConvert.DeserializeObject<List<BarberosAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<Barberos> Guardar(Barberos entidad, string rol)
        {
            if (entidad.IdBarbero != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Barberos?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new Barberos();

            return JsonConvert.DeserializeObject<Barberos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Barberos Modificar(Barberos entidad, string rol)
        {
            if (entidad.IdBarbero == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Barberos?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Barberos();

            return JsonConvert.DeserializeObject<Barberos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Barberos Eliminar(Barberos entidad, string rol)
        {
            if (entidad.IdBarbero == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Barberos?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Barberos();

            return JsonConvert.DeserializeObject<Barberos>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
