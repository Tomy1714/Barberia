using Microsoft.EntityFrameworkCore;
using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;

namespace Lib_Negocio.Implementacion
{
    public class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        // DbSets
        public DbSet<Personas>             Personas             { get; set; }
        public DbSet<Clientes>             Clientes             { get; set; }
        public DbSet<Empleados>            Empleados            { get; set; }
        public DbSet<Barberos>             Barberos            { get; set; }
        public DbSet<Recepcionistas>       Recepcionistas       { get; set; }
        public DbSet<Administradores>      Administradores      { get; set; }
        public DbSet<Sedes>                Sedes                { get; set; }
        public DbSet<Servicios>            Servicios            { get; set; }
        public DbSet<ServiciosCorte>       ServiciosCorte       { get; set; }
        public DbSet<ServiciosTratamiento> ServiciosTratamiento { get; set; }
        public DbSet<Combos>               Combos               { get; set; }
        public DbSet<Productos>            Productos            { get; set; }
        public DbSet<Inventarios>          Inventarios          { get; set; }
        public DbSet<InventarioProductos>  InventarioProductos  { get; set; }
        public DbSet<Turnos>               Turnos               { get; set; }
        public DbSet<Horarios>             Horarios             { get; set; }
        public DbSet<HorarioDias>         HorarioDias         { get; set; }
        public DbSet<Citas>                Citas                { get; set; }
        public DbSet<Pagos>                Pagos                { get; set; }
        public DbSet<Facturas>             Facturas             { get; set; }
        public DbSet<Calificaciones>       Calificaciones       { get; set; }
        public DbSet<Promociones>          Promociones          { get; set; }
        public DbSet<Notificaciones>       Notificaciones       { get; set; }
        public DbSet<PuntosFidelidad>      PuntosFidelidad      { get; set; }
        public DbSet<EmpleadoSede>       EmpleadoSede     { get; set; }
        public DbSet<ComboServicios>       ComboServicios       { get; set; }
        public DbSet<Usuarios>             Usuarios             { get; set; }

        public DbSet<PersonasAuditoria> PersonasAuditoria { get; set; }
        public DbSet<ClientesAuditoria> ClientesAuditoria { get; set; }
        public DbSet<EmpleadosAuditoria> EmpleadosAuditoria { get; set; }
        public DbSet<BarberosAuditoria> BarberosAuditoria { get; set; }
        public DbSet<RecepcionistasAuditoria> RecepcionistasAuditoria { get; set; }
        public DbSet<AdministradoresAuditoria> AdministradoresAuditoria { get; set; }
        public DbSet<SedesAuditoria> SedesAuditoria { get; set; }
        public DbSet<ServiciosAuditoria> ServiciosAuditoria { get; set; }
        public DbSet<ServiciosCorteAuditoria> ServiciosCorteAuditoria { get; set; }
        public DbSet<ServiciosTratamientoAuditoria> ServiciosTratamientoAuditoria { get; set; }
        public DbSet<CombosAuditoria> CombosAuditoria { get; set; }
        public DbSet<ProductosAuditoria> ProductosAuditoria { get; set; }
        public DbSet<InventariosAuditoria> InventariosAuditoria { get; set; }
        public DbSet<InventarioProductosAuditoria> InventarioProductosAuditoria { get; set; }
        public DbSet<TurnosAuditoria> TurnosAuditoria { get; set; }
        public DbSet<HorariosAuditoria> HorariosAuditoria { get; set; }
        public DbSet<HorariosDiasAuditoria> HorariosDiasAuditoria { get; set; }
        public DbSet<CitasAuditoria> CitasAuditoria { get; set; }
        public DbSet<PagosAuditoria> PagosAuditoria { get; set; }
        public DbSet<FacturasAuditoria> FacturasAuditoria { get; set; }
        public DbSet<CalificacionesAuditoria> CalificacionesAuditoria { get; set; }
        public DbSet<PromocionesAuditoria> PromocionesAuditoria { get; set; }
        public DbSet<NotificacionesAuditoria> NotificacionesAuditoria { get; set; }
        public DbSet<PuntosFidelidadAuditoria> PuntosFidelidadAuditoria { get; set; }
        public DbSet<EmpleadosSedesAuditoria> EmpleadosSedesAuditoria { get; set; }
        public DbSet<ComboServiciosAuditoria> ComboServiciosAuditoria { get; set; }
        public DbSet<UsuariosAuditoria> UsuariosAuditoria { get; set; }

    }
}
