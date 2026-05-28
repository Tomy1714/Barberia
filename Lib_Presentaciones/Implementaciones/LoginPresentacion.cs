using Lib_Negocio.Entidades;
using LibPresentaciones.Interfaces;
using Newtonsoft.Json;

namespace LibPresentaciones.Implementaciones
{
    public class LoginPresentacion : ILoginPresentacion
    {
        private IComunicaciones? icomunicaciones;

        public async Task<Usuarios?> Login(string email, string contrasena)
        {
            var entidad = new Usuarios
            {
                Email      = email,
                Contrasena = contrasena
            };

            var datos = new Dictionary<string, object>();
            datos["Url"]     = "https://localhost:7179/api/Login";
            datos["Entidad"] = entidad;

            this.icomunicaciones = new Comunicaciones();
            var respuesta = await this.icomunicaciones.EjecutarGuardar(datos);

            if (!respuesta.ContainsKey("Valor"))
                return null;

            return JsonConvert.DeserializeObject<Usuarios>(
                respuesta["Valor"].ToString()!);
        }
    }
}
