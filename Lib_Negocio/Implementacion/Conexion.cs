using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        public DbSet<Barberos>             Barberos             { get; set; }
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
        public DbSet<HorariosDias>         HorariosDias         { get; set; }
        public DbSet<Citas>                Citas                { get; set; }
        public DbSet<Pagos>                Pagos                { get; set; }
        public DbSet<Facturas>             Facturas             { get; set; }
        public DbSet<Calificaciones>       Calificaciones       { get; set; }
        public DbSet<Promociones>          Promociones          { get; set; }
        public DbSet<Notificaciones>       Notificaciones       { get; set; }
        public DbSet<PuntosFidelidad>      PuntosFidelidad      { get; set; }
        public DbSet<EmpleadosSedes>       EmpleadosSedes       { get; set; }
        public DbSet<ComboServicios>       ComboServicios       { get; set; }
    }
}
