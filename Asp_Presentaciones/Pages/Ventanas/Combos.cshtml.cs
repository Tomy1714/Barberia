using Asp_Presentaciones.Infraestructura;
using LibPresentaciones.Implementaciones;
using Lib_Negocio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Presentaciones.Pages.Ventanas
{
    public class CombosModel : PaginaBase
    {
        private readonly CombosPresentacion _negocio = new();

        public List<Combos> Lista { get; private set; } = new();
        public List<ServiciosCorte> ListaCortes { get; private set; } = new();
        public List<ServiciosTratamiento> ListaTratamientos { get; private set; } = new();
        public List<Servicios> ListaServicios { get; private set; } = new();
        public List<ComboServicios> ListaComboServicios { get; private set; } = new();
        public List<FilaAuditoria> Auditoria { get; private set; } = new();

        [BindProperty] public Combos Item { get; set; } = new();
        [BindProperty] public int IdServicioCorte { get; set; }
        [BindProperty] public int IdServicioTrat { get; set; }
        [TempData] public string? Mensaje { get; set; }
        [TempData] public string? ErrorMsg { get; set; }

      
        public decimal? PrecioFinalCalculado { get; private set; }
        public decimal? AhorroCalculado { get; private set; }
        public string InfoCombo { get; private set; } = "";

        public IActionResult OnGet()
        {
            var redir = ValidarAcceso("Administrador", "Recepcionista","Cliente","Barbero");
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
                if (IdServicioCorte == 0 || IdServicioTrat == 0)
                    throw new Exception("Debes seleccionar un corte y un tratamiento.");

                if (Item.DescuentoCombo < 1 || Item.DescuentoCombo > 100)
                    throw new Exception("El descuento debe estar entre 1% y 100%.");

                Cargar();

         
                var corte = ListaCortes.FirstOrDefault(c => c.IdServicioCorte == IdServicioCorte);
                if (corte == null) throw new Exception("Corte no encontrado.");
                var servCorte = ListaServicios.FirstOrDefault(s => s.IdServicio == corte.IdServicio);
                if (servCorte == null) throw new Exception("Servicio de corte no encontrado.");

                
                var trat = ListaTratamientos.FirstOrDefault(t => t.IdServicioTratamiento == IdServicioTrat);
                if (trat == null) throw new Exception("Tratamiento no encontrado.");
                var servTrat = ListaServicios.FirstOrDefault(s => s.IdServicio == trat.IdServicio);
                if (servTrat == null) throw new Exception("Servicio de tratamiento no encontrado.");

                
                decimal precioTotal = servCorte.PrecioBase + servTrat.PrecioBase;

                Item.IdServicio = corte.IdServicio;
                Item.Descripcion = string.IsNullOrEmpty(Item.Descripcion)
                    ? $"{servCorte.Nombre} + {servTrat.Nombre}"
                    : Item.Descripcion;

            
                Combos comboGuardado;
                if (Item.IdCombo == 0)
                    comboGuardado = await _negocio.Guardar(Item, RolActual);
                else
                {
                    comboGuardado = _negocio.Modificar(Item, RolActual);
                }

             
                var csCorte = new CombosPresentacion();
                var comboServCorte = new ComboServicios
                {
                    IdCombo = comboGuardado.IdCombo,
                    IdServicio = corte.IdServicio
                };
                var comboServTrat = new ComboServicios
                {
                    IdCombo = comboGuardado.IdCombo,
                    IdServicio = trat.IdServicio
                };
                await new ComboServiciosPresentacion().Guardar(comboServCorte, RolActual);
                await new ComboServiciosPresentacion().Guardar(comboServTrat, RolActual);

                Mensaje = $"Combo guardado: {Item.Descripcion} — Precio base: ${precioTotal:N0} con {Item.DescuentoCombo}% descuento.";
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
                Mensaje = "Combo eliminado correctamente.";
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return RedirectToPage();
        }

      
        public IActionResult OnPostCalcularPrecio()
        {
            var redir = ValidarAcceso("Administrador", "Recepcionista", "Barbero", "Cliente");
            if (redir != null) return redir;
            try
            {
                Cargar();

              
                var serviciosDelCombo = ListaComboServicios
                    .Where(cs => cs.IdCombo == Item.IdCombo)
                    .Select(cs => ListaServicios.FirstOrDefault(s => s.IdServicio == cs.IdServicio))
                    .Where(s => s != null)
                    .ToList();

                if (!serviciosDelCombo.Any())
                {
               
                    var servBase = ListaServicios.FirstOrDefault(s => s.IdServicio == Item.IdServicio);
                    if (servBase != null) serviciosDelCombo.Add(servBase);
                }

                decimal precioBase = serviciosDelCombo.Sum(s => s!.PrecioBase);
                PrecioFinalCalculado = precioBase * (1 - Item.DescuentoCombo / 100);
                AhorroCalculado = precioBase - PrecioFinalCalculado;
                InfoCombo = string.Join(" + ", serviciosDelCombo.Select(s => s!.Nombre));
            }
            catch (Exception ex) { ErrorMsg = ex.Message; }
            return Page();
        }


        public string NombreServicio(int idServicio)
        {
            var s = ListaServicios.FirstOrDefault(x => x.IdServicio == idServicio);
            return s == null ? $"#{idServicio}" : s.Nombre;
        }

        public string NombreCorte(int idServicioCorte)
        {
            var c = ListaCortes.FirstOrDefault(x => x.IdServicioCorte == idServicioCorte);
            if (c == null) return "";
            var s = ListaServicios.FirstOrDefault(x => x.IdServicio == c.IdServicio);
            return s == null ? $"Corte #{idServicioCorte}" : $"{s.Nombre} ({c.TipoCorte})";
        }

        public string NombreTratamiento(int idServicioTrat)
        {
            var t = ListaTratamientos.FirstOrDefault(x => x.IdServicioTratamiento == idServicioTrat);
            if (t == null) return "";
            var s = ListaServicios.FirstOrDefault(x => x.IdServicio == t.IdServicio);
            return s == null ? $"Tratamiento #{idServicioTrat}" : $"{s.Nombre} ({t.TipoTratamiento})";
        }

        private void Cargar()
        {
            try
            {
                Lista = _negocio.Consultar(RolActual) ?? new();
                ListaCortes = new ServiciosCortePresentacion().Consultar(RolActual) ?? new();
                ListaTratamientos = new ServiciosTratamientoPresentacion().Consultar(RolActual) ?? new();
                ListaServicios = new ServiciosPresentacion().Consultar(RolActual) ?? new();
                ListaComboServicios = new ComboServiciosPresentacion().Consultar(RolActual) ?? new();
                if (EsAdministrador)
                    Auditoria = (_negocio.ConsultarAuditoria(RolActual) ?? new())
                        .Select(a => new FilaAuditoria
                        {
                            IdAuditoria = a.IdAuditoria,
                            IdReferencia = a.IdCombo,
                            Accion = a.Accion ?? "",
                            Fecha = a.Fecha
                        }).ToList();
            }
            catch (Exception ex) { ErrorMsg = "No se pudo conectar con el API: " + ex.Message; }
        }
    }
}
