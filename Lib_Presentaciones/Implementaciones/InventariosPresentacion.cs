using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class InventariosPresentacion : IInventariosPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<Inventarios> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Inventarios?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Inventarios>();

            return JsonConvert.DeserializeObject<List<Inventarios>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<InventariosAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Inventarios/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<InventariosAuditoria>();

            return JsonConvert.DeserializeObject<List<InventariosAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<Inventarios> Guardar(Inventarios entidad, string rol)
        {
            if (entidad.IdInventario != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Inventarios?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new Inventarios();

            return JsonConvert.DeserializeObject<Inventarios>(
                respuesta["Valor"].ToString()!)!;
        }

        public Inventarios Modificar(Inventarios entidad, string rol)
        {
            if (entidad.IdInventario == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Inventarios?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Inventarios();

            return JsonConvert.DeserializeObject<Inventarios>(
                respuesta["Valor"].ToString()!)!;
        }

        public Inventarios Eliminar(Inventarios entidad, string rol)
        {
            if (entidad.IdInventario == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Inventarios?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Inventarios();

            return JsonConvert.DeserializeObject<Inventarios>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
