using Asp_Presentaciones.Infraestructura;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Presentaciones.Pages
{
    public class AccesoDenegadoModel : PaginaBase
    {
        public IActionResult OnGet()
        {
            if (!Autenticado)
                return RedirectToPage("/Account/Login");
            return Page();
        }
    }
}
