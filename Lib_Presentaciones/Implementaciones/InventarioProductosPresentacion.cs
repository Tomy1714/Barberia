using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class InventarioProductosPresentacion : IInventarioProductosPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<InventarioProductos> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/InventarioProductos?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<InventarioProductos>();

            return JsonConvert.DeserializeObject<List<InventarioProductos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<InventarioProductosAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/InventarioProductos/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<InventarioProductosAuditoria>();

            return JsonConvert.DeserializeObject<List<InventarioProductosAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<InventarioProductos> Guardar(InventarioProductos entidad, string rol)
        {
            if (entidad.IdInventarioProducto != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/InventarioProductos?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new InventarioProductos();

            return JsonConvert.DeserializeObject<InventarioProductos>(
                respuesta["Valor"].ToString()!)!;
        }

        public InventarioProductos Modificar(InventarioProductos entidad, string rol)
        {
            if (entidad.IdInventarioProducto == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/InventarioProductos?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new InventarioProductos();

            return JsonConvert.DeserializeObject<InventarioProductos>(
                respuesta["Valor"].ToString()!)!;
        }

        public InventarioProductos Eliminar(InventarioProductos entidad, string rol)
        {
            if (entidad.IdInventarioProducto == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/InventarioProductos?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new InventarioProductos();

            return JsonConvert.DeserializeObject<InventarioProductos>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
