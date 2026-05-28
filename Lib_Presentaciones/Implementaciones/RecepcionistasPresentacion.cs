using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class RecepcionistasPresentacion : IRecepcionistasPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<Recepcionistas> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Recepcionistas?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Recepcionistas>();

            return JsonConvert.DeserializeObject<List<Recepcionistas>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<RecepcionistasAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Recepcionistas/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<RecepcionistasAuditoria>();

            return JsonConvert.DeserializeObject<List<RecepcionistasAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<Recepcionistas> Guardar(Recepcionistas entidad, string rol)
        {
            if (entidad.IdRecepcionista != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Recepcionistas?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new Recepcionistas();

            return JsonConvert.DeserializeObject<Recepcionistas>(
                respuesta["Valor"].ToString()!)!;
        }

        public Recepcionistas Modificar(Recepcionistas entidad, string rol)
        {
            if (entidad.IdRecepcionista == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Recepcionistas?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Recepcionistas();

            return JsonConvert.DeserializeObject<Recepcionistas>(
                respuesta["Valor"].ToString()!)!;
        }

        public Recepcionistas Eliminar(Recepcionistas entidad, string rol)
        {
            if (entidad.IdRecepcionista == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Recepcionistas?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Recepcionistas();

            return JsonConvert.DeserializeObject<Recepcionistas>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
