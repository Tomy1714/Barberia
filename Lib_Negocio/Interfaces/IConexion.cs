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

        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
