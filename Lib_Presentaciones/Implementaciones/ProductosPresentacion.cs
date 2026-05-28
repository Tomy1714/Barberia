using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class ProductosPresentacion : IProductosPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<Productos> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Productos?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Productos>();

            return JsonConvert.DeserializeObject<List<Productos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<ProductosAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Productos/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<ProductosAuditoria>();

            return JsonConvert.DeserializeObject<List<ProductosAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<Productos> Guardar(Productos entidad, string rol)
        {
            if (entidad.IdProducto != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Productos?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new Productos();

            return JsonConvert.DeserializeObject<Productos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Productos Modificar(Productos entidad, string rol)
        {
            if (entidad.IdProducto == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Productos?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Productos();

            return JsonConvert.DeserializeObject<Productos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Productos Eliminar(Productos entidad, string rol)
        {
            if (entidad.IdProducto == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Productos?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Productos();

            return JsonConvert.DeserializeObject<Productos>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
