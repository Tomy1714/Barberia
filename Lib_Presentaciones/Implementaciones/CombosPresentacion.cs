using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class CombosPresentacion : ICombosPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public List<Combos> Consultar(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Combos?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<Combos>();

            return JsonConvert.DeserializeObject<List<Combos>>(
                respuesta["Valor"].ToString()!)!;
        }

        public List<CombosAuditoria> ConsultarAuditoria(string rol)
        {
            var datos = new Dictionary<string, object>();
            datos["Url"] = "https://localhost:7179/api/Combos/auditoria?rol=" + rol;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarConsultar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new List<CombosAuditoria>();

            return JsonConvert.DeserializeObject<List<CombosAuditoria>>(
                respuesta["Valor"].ToString()!)!;
        }

        public async Task<Combos> Guardar(Combos entidad, string rol)
        {
            if (entidad.IdCombo != 0)
                throw new Exception("El registro ya fue guardado");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Combos?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return new Combos();

            return JsonConvert.DeserializeObject<Combos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Combos Modificar(Combos entidad, string rol)
        {
            if (entidad.IdCombo == 0)
                throw new Exception("No existe el registro a modificar");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Combos?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarModificar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Combos();

            return JsonConvert.DeserializeObject<Combos>(
                respuesta["Valor"].ToString()!)!;
        }

        public Combos Eliminar(Combos entidad, string rol)
        {
            if (entidad.IdCombo == 0)
                throw new Exception("No tiene ID para eliminarse");

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Combos?rol=" + rol;
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var task = this.icomunicaciones.EjecutarEliminar(datos)!;
            task.Wait();
            var respuesta = task.Result;

            if (!respuesta.ContainsKey("Valor"))
                return new Combos();

            return JsonConvert.DeserializeObject<Combos>(
                respuesta["Valor"].ToString()!)!;
        }
    }
}
