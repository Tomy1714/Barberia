using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_Presentaciones.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty] public string Email { get; set; } = "";
        [BindProperty] public string Contrasena { get; set; } = "";
        public string? Error { get; set; }

        public IActionResult OnGet()
        {
            if (Sesion.Autenticado(HttpContext.Session))
                return RedirectToPage("/Index");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Contrasena))
            {
                Error = "Ingresa correo y contraseña.";
                return Page();
            }

            try
            {
                var login = new LoginPresentacion();
                var usuario = await login.Login(Email.Trim(), Contrasena);

                if (usuario == null || string.IsNullOrEmpty(usuario.Rol))
                {
                    Error = "Credenciales incorrectas.";
                    return Page();
                }

                Sesion.Guardar(HttpContext.Session, new Usuariosesion
                {
                    IdUsuario = usuario.IdUsuario,
                    IdPersona = usuario.IdPersona,
                    Email     = usuario.Email,
                    Rol       = usuario.Rol
                });

                return RedirectToPage("/Index");
            }
            catch
            {
                Error = "No se pudo iniciar sesión. Verifica tus credenciales o que el API esté en línea.";
                return Page();
            }
        }
    }
}
