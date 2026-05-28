using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class CitasPresentacion : ICitasPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<Citas> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Citas?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Citas>();

            return JsonConvert.DeserializeObject<List<Citas>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<CitasAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Citas/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<CitasAuditoria>();

            return JsonConvert.DeserializeObject<List<CitasAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<Citas> Guardar(Citas entidad, string rol)
        {
            if (entidad.IdCita != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Citas?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new Citas();

            return JsonConvert.DeserializeObject<Citas>(
                respuesta["Valor"].ToString()!)!;
        }

        public Citas Modificar(Citas entidad, string rol)
        {
            if (entidad.IdCita == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Citas?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Citas();

            return JsonConvert.DeserializeObject<Citas>(
                respuesta["Valor"].ToString()!)!;
        }

        public Citas Eliminar(Citas entidad, string rol)
        {
            if (entidad.IdCita == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Citas?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Citas();

            return JsonConvert.DeserializeObject<Citas>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
