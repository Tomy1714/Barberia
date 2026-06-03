using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Lib_Negocio.Entidades;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;


namespace Asp_Presentaciones.Pages.Ventanas
{
    public class ServiciosModel : PaginaBase
    {
        private readonly ServiciosPresentacion _negocio = new();

        public List<Servicios> Lista { get; private set; } = new();
        public List<FilaAuditoria> Auditoria { get; private set; } = new();

        [BindProperty] public Servicios Item { get; set; } = new();
        [TempData] public string? Mensaje { get; set; }
        [TempData] public string? ErrorMsg { get; set; }

        public IActionResult OnGet()
        {
            var redir = ValidarAcceso();   
            if (redir != null) return redir;
            Cargar();
            return Page();
        }

        public async Task<IActionResult> OnPostGuardarAsync()
        {
            var redir = ValidarAcceso("Administrador");
            if (redir != null) return redir;
            try
            {
                if (Item.IdServicio == 0) await _negocio.Guardar(Item, RolActual);
                else _negocio.Modificar(Item, RolActual);
                Mensaje = "Servicio guardado correctamente.";
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

        public IActionResult OnPostEliminar()
        {
            var redir = ValidarAcceso("Administrador");
            if (redir != null) return redir;
            try
            {
                _negocio.Eliminar(Item, RolActual);
                Mensaje = "Servicio eliminado.";
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }


        public IActionResult OnGetExportarPdf()
        {
            Cargar();

            byte[] pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Header()
                        .Text("Reporte de Servicios - BarberPro")
                        .FontSize(20)
                        .Bold();

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(50);
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.ConstantColumn(80);
                            columns.ConstantColumn(80);
                        });

                        tabla.Header(header =>
                        {
                            header.Cell().Text("ID");
                            header.Cell().Text("Nombre");
                            header.Cell().Text("Descripción");
                            header.Cell().Text("Precio");
                            header.Cell().Text("Duración");
                        });

                        foreach (var s in Lista)
                        {
                            tabla.Cell().Text(s.IdServicio.ToString());
                            tabla.Cell().Text(s.Nombre);
                            tabla.Cell().Text(s.Descripcion);
                            tabla.Cell().Text("$" + s.PrecioBase.ToString("N0"));
                            tabla.Cell().Text(s.DuracionMinutos + " min");
                        }
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}");
                });
            }).GeneratePdf();

            return File(pdf, "application/pdf", "Servicios.pdf");
        }




        private void Cargar()
        {
            try
            {
                Lista = _negocio.Consultar(RolActual) ?? new();
                if (EsAdministrador)
                    Auditoria = (_negocio.ConsultarAuditoria(RolActual) ?? new())
                        .Select(a => new FilaAuditoria
                        {
                            IdAuditoria = a.IdAuditoria, IdReferencia = a.IdServicio,
                            Accion = a.Accion ?? "", Fecha = a.Fecha
                        }).ToList();
            }
            catch (Exception ex) { ErrorMsg = "No se pudo conectar con el API: " + ex.Message; }
        }
    }
}
