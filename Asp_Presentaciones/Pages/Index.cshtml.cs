using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Lib_Negocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Presentaciones.Pages
{
    public class IndexModel : PaginaBase
    {
        public int TotalCitas { get; private set; }
        public int TotalClientes { get; private set; }
        public int TotalBarberos { get; private set; }
        public int TotalServicios { get; private set; }
        public int TotalSedes { get; private set; }
        public int CitasHoy { get; private set; }

        public List<Citas> CitasRecientes { get; private set; } = new();
        public string? Aviso { get; private set; }

        public IActionResult OnGet()
        {
            var redir = ValidarAcceso();
            if (redir != null) return redir;

            try
            {
                var citas = new CitasPresentacion().Consultar(RolActual) ?? new List<Citas>();
                TotalCitas = citas.Count;
                CitasHoy = citas.Count(c => c.FechaHoraInicio?.Date == DateTime.Today);
                CitasRecientes = citas
                    .OrderByDescending(c => c.FechaHoraInicio ?? DateTime.MinValue)
                    .Take(8).ToList();

                if (EsAdministrador || EsRecepcionista)
                    TotalClientes = (new ClientesPresentacion().Consultar(RolActual) ?? new()).Count;
                if (EsAdministrador || EsRecepcionista)
                    TotalBarberos = (new BarberosPresentacion().Consultar(RolActual) ?? new()).Count;

                TotalServicios = (new ServiciosPresentacion().Consultar(RolActual) ?? new()).Count;
                TotalSedes = (new SedesPresentacion().Consultar(RolActual) ?? new()).Count;
            }
            catch
            {
                Aviso = "No se pudo conectar con el API (¿está corriendo en https://localhost:7179?). El panel se muestra sin datos.";
            }

            return Page();
        }
    }
}
