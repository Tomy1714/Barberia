using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class FacturasPresentacion : IFacturasPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<Facturas> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Facturas?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Facturas>();

            return JsonConvert.DeserializeObject<List<Facturas>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<FacturasAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Facturas/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<FacturasAuditoria>();

            return JsonConvert.DeserializeObject<List<FacturasAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<Facturas> Guardar(Facturas entidad, string rol)
        {
            if (entidad.IdFactura != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Facturas?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new Facturas();

            return JsonConvert.DeserializeObject<Facturas>(
                respuesta["Valor"].ToString()!)!;
        }

        public Facturas Modificar(Facturas entidad, string rol)
        {
            if (entidad.IdFactura == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Facturas?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Facturas();

            return JsonConvert.DeserializeObject<Facturas>(
                respuesta["Valor"].ToString()!)!;
        }

        public Facturas Eliminar(Facturas entidad, string rol)
        {
            if (entidad.IdFactura == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Facturas?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Facturas();

            return JsonConvert.DeserializeObject<Facturas>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
