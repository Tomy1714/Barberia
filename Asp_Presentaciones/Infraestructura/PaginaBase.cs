using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_Presentaciones.Infraestructura
{
    /// <summary>
    /// Base de las paginas seguras: expone datos de sesion y valida el rol.
    /// </summary>
    public abstract class PaginaBase : PageModel
    {
        public string RolActual    => Sesion.Rol(HttpContext.Session);
        public string EmailActual  => Sesion.Email(HttpContext.Session);
        public int    IdUsuario    => Sesion.IdUsuario(HttpContext.Session);
        public int    IdPersona    => Sesion.IdPersona(HttpContext.Session);
        public bool   Autenticado  => Sesion.Autenticado(HttpContext.Session);

        public bool EsAdministrador => RolActual == "Administrador";
        public bool EsRecepcionista => RolActual == "Recepcionista";
        public bool EsBarbero       => RolActual == "Barbero";
        public bool EsCliente       => RolActual == "Cliente";

        /// <summary>
        /// Devuelve un redirect si el usuario no esta autenticado o su rol no esta
        /// dentro de los permitidos; null si tiene acceso. Si no se pasan roles,
        /// solo exige sesion iniciada.
        /// </summary>
        protected IActionResult? ValidarAcceso(params string[] rolesPermitidos)
        {
            if (!Autenticado)
                return RedirectToPage("/Account/Login");

            if (rolesPermitidos.Length > 0 && !rolesPermitidos.Contains(RolActual))
                return RedirectToPage("/AccesoDenegado");

            return null;
        }
    }
}
