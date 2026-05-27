using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

       
        DbSet<Personas>             Personas             { get; set; }
        DbSet<Clientes>             Clientes             { get; set; }
        DbSet<Empleados>            Empleados            { get; set; }
        DbSet<Barberos>             Barberos             { get; set; }
        DbSet<Recepcionistas>       Recepcionistas       { get; set; }
        DbSet<Administradores>      Administradores      { get; set; }
        DbSet<Sedes>                Sedes                { get; set; }
        DbSet<Servicios>            Servicios            { get; set; }
        DbSet<ServiciosCorte>       ServiciosCorte       { get; set; }
        DbSet<ServiciosTratamiento> ServiciosTratamiento { get; set; }
        DbSet<Combos>               Combos               { get; set; }
        DbSet<Productos>            Productos            { get; set; }
        DbSet<Inventarios>          Inventarios          { get; set; }
        DbSet<InventarioProductos>  InventarioProductos  { get; set; }
        DbSet<Turnos>               Turnos               { get; set; }
        DbSet<Horarios>             Horarios             { get; set; }
        DbSet<HorarioDias>         HorarioDias         { get; set; }
        DbSet<Citas>                Citas                { get; set; }
        DbSet<Pagos>                Pagos                { get; set; }
        DbSet<Facturas>             Facturas             { get; set; }
        DbSet<Calificaciones>       Calificaciones       { get; set; }
        DbSet<Promociones>          Promociones          { get; set; }
        DbSet<Notificaciones>       Notificaciones       { get; set; }
        DbSet<PuntosFidelidad>      PuntosFidelidad      { get; set; }
        DbSet<EmpleadoSede>       EmpleadoSede      { get; set; }
        DbSet<ComboServicios>       ComboServicios       { get; set; }

        DbSet<Usuarios> Usuarios { get; set; }

        DbSet<PersonasAuditoria> PersonasAuditoria { get; set; }
        DbSet<ClientesAuditoria> ClientesAuditoria { get; set; }
        DbSet<EmpleadosAuditoria> EmpleadosAuditoria { get; set; }
        DbSet<BarberosAuditoria> BarberosAuditoria { get; set; }
        DbSet<RecepcionistasAuditoria> RecepcionistasAuditoria { get; set; }
        DbSet<AdministradoresAuditoria> AdministradoresAuditoria { get; set; }
        DbSet<SedesAuditoria> SedesAuditoria { get; set; }
        DbSet<ServiciosAuditoria> ServiciosAuditoria { get; set; }
        DbSet<ServiciosCorteAuditoria> ServiciosCorteAuditoria { get; set; }
        DbSet<ServiciosTratamientoAuditoria> ServiciosTratamientoAuditoria { get; set; }
        DbSet<CombosAuditoria> CombosAuditoria { get; set; }
        DbSet<ProductosAuditoria> ProductosAuditoria { get; set; }
        DbSet<InventariosAuditoria> InventariosAuditoria { get; set; }
        DbSet<InventarioProductosAuditoria> InventarioProductosAuditoria { get; set; }
        DbSet<TurnosAuditoria> TurnosAuditoria { get; set; }
        DbSet<HorariosAuditoria> HorariosAuditoria { get; set; }
        DbSet<HorariosDiasAuditoria> HorariosDiasAuditoria { get; set; }
        DbSet<CitasAuditoria> CitasAuditoria { get; set; }
        DbSet<PagosAuditoria> PagosAuditoria { get; set; }
        DbSet<FacturasAuditoria> FacturasAuditoria { get; set; }
        DbSet<CalificacionesAuditoria> CalificacionesAuditoria { get; set; }
        DbSet<PromocionesAuditoria> PromocionesAuditoria { get; set; }
        DbSet<NotificacionesAuditoria> NotificacionesAuditoria { get; set; }
        DbSet<PuntosFidelidadAuditoria> PuntosFidelidadAuditoria { get; set; }
        DbSet<EmpleadosSedesAuditoria> EmpleadosSedesAuditoria { get; set; }
        DbSet<ComboServiciosAuditoria> ComboServiciosAuditoria { get; set; }
        DbSet<UsuariosAuditoria> UsuariosAuditoria { get; set; }


        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
