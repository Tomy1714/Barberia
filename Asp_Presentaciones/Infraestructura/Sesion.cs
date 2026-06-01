using Microsoft.AspNetCore.Http;

namespace Asp_Presentaciones.Infraestructura
{

    public static class Sesion
    {
        public const string ClaveRol      = "Rol";
        public const string ClaveEmail    = "Email";
        public const string ClaveIdUsuario = "IdUsuario";
        public const string ClaveIdPersona = "IdPersona";

        public static void Guardar(ISession sesion, Usuariosesion usuario)
        {
            sesion.SetString(ClaveRol, usuario.Rol);
            sesion.SetString(ClaveEmail, usuario.Email);
            sesion.SetInt32(ClaveIdUsuario, usuario.IdUsuario);
            sesion.SetInt32(ClaveIdPersona, usuario.IdPersona);
        }

        public static void Limpiar(ISession sesion) => sesion.Clear();

        public static bool Autenticado(ISession sesion) =>
            !string.IsNullOrEmpty(sesion.GetString(ClaveRol));

        public static string Rol(ISession sesion) => sesion.GetString(ClaveRol) ?? "";
        public static string Email(ISession sesion) => sesion.GetString(ClaveEmail) ?? "";
        public static int IdUsuario(ISession sesion) => sesion.GetInt32(ClaveIdUsuario) ?? 0;
        public static int IdPersona(ISession sesion) => sesion.GetInt32(ClaveIdPersona) ?? 0;
    }

    public class Usuariosesion
    {
        public int    IdUsuario { get; set; }
        public int    IdPersona { get; set; }
        public string Email     { get; set; } = "";
        public string Rol       { get; set; } = "";
    }
}
