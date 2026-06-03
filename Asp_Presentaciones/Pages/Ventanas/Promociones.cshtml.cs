using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Lib_Negocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Presentaciones.Pages.Ventanas
{
    public class PromocionesModel : PaginaBase
    {
        private readonly PromocionesPresentacion _negocio = new();

        public List<Promociones> Lista { get; private set; } = new();
        public List<Servicios> Servicios { get; private set; } = new();
        public List<FilaAuditoria> Auditoria { get; private set; } = new();

        [BindProperty] public decimal PorcentajeDescuento { get; set; }
        [BindProperty] public Promociones Item { get; set; } = new();
        [BindProperty] public decimal PrecioBase { get; set; }
        [TempData] public string? Mensaje { get; set; }
        [TempData] public string? ErrorMsg { get; set; }

       
        public decimal? DescuentoCalculado { get; private set; }
        public decimal? PrecioFinalCalculado { get; private set; }
        public string InfoPromocion { get; private set; } = "";

        public static readonly Dictionary<string, string> FechasEspeciales = new()
        {
            { "Día del Padre",    "Tercer domingo de junio" },
            { "Día del Hombre",   "19 de noviembre" },
            { "Día del Niño",     "31 de octubre" },
            { "Año Nuevo",        "1 de enero" },
            { "Navidad",          "24-25 de diciembre" },
            { "Halloween",        "31 de octubre" },
            { "San Valentín",     "14 de febrero" },
            { "Día de la Madre",  "Segundo domingo de mayo" },
            { "Personalizada",    "Fecha personalizada" },
        };

        public IActionResult OnGet()
        {
            var redir = ValidarAcceso("Administrador", "Recepcionista");
            if (redir != null) return redir;
            Cargar();
            return Page();
        }

        public async Task<IActionResult> OnPostGuardarAsync()
        {
            var redir = ValidarAcceso("Administrador", "Recepcionista");
            if (redir != null) return redir;
            try
            {
                if (Item.IdServicio == 0)
                    throw new Exception("Debes seleccionar un servicio.");
                if (string.IsNullOrEmpty(Item.Nombre))
                    throw new Exception("El nombre de la promoción es obligatorio.");
                if (Item.PorcentajeDescuento < 1 || Item.PorcentajeDescuento > 100)
                    throw new Exception("El descuento debe estar entre 1% y 100%.");
                if (Item.FechaInicio >= Item.FechaFin)
                    throw new Exception("La fecha de inicio debe ser anterior a la fecha de fin.");

                if (Item.IdPromocion == 0) await _negocio.Guardar(Item, RolActual);
                else _negocio.Modificar(Item, RolActual);

                Mensaje = $"Promoción '{Item.Nombre}' guardada correctamente.";
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

        public IActionResult OnPostEliminar()
        {
            var redir = ValidarAcceso("Administrador", "Recepcionista");
            if (redir != null) return redir;
            try
            {
                _negocio.Eliminar(Item, RolActual);
                Mensaje = "Promoción eliminada correctamente.";
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }


        public IActionResult OnPostCalcularDescuento()
        {
            var redir = ValidarAcceso("Administrador", "Recepcionista", "Barbero", "Cliente");
            if (redir != null) return redir;

            try
            {
                Cargar();

                var promocion = Lista.FirstOrDefault(x => x.IdPromocion == Item.IdPromocion);

                if (promocion == null)
                    throw new Exception("Promoción no encontrada.");

                var servicio = Servicios.FirstOrDefault(x => x.IdServicio == promocion.IdServicio);

                if (servicio == null)
                    throw new Exception("Servicio no encontrado.");

                decimal precioBase = servicio.PrecioBase;

                DescuentoCalculado =
                    precioBase * (promocion.PorcentajeDescuento / 100);

                PrecioFinalCalculado =
                    precioBase - DescuentoCalculado;

                InfoPromocion =
                    $"{promocion.Nombre} - {promocion.PorcentajeDescuento}% de descuento sobre {servicio.Nombre}";
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }

            return Page();
        }

        public string NombreServicio(int idServicio)
        {
            var s = Servicios.FirstOrDefault(x => x.IdServicio == idServicio);
            return s == null ? $"#{idServicio}" : $"{s.Nombre}";
        }

        public bool EstaVigente(Promociones p) =>
            p.Activa &&
            DateTime.Today >= p.FechaInicio.Date &&
            DateTime.Today <= p.FechaFin.Date;

        private void Cargar()
        {
            try
            {
                Lista = _negocio.Consultar(RolActual) ?? new();
                Servicios = new ServiciosPresentacion().Consultar(RolActual) ?? new();
                if (EsAdministrador)
                    Auditoria = (_negocio.ConsultarAuditoria(RolActual) ?? new())
                        .Select(a => new FilaAuditoria
                        {
                            IdAuditoria = a.IdAuditoria,
                            IdReferencia = a.IdPromocion,
                            Accion = a.Accion ?? "",
                            Fecha = a.Fecha
                        }).ToList();
            }
            catch (Exception ex) { ErrorMsg = "No se pudo conectar con el API: " + ex.Message; }
        }
    }
}
