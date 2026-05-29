using Asp_Presentaciones.Infraestructura;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_Presentaciones.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()  => Cerrar();
        public IActionResult OnPost() => Cerrar();

        private IActionResult Cerrar()
        {
            Sesion.Limpiar(HttpContext.Session);
            return RedirectToPage("/Account/Login");
        }
    }
}
