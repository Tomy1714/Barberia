using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface ICalificacionesPresentacion
    {
        List<Calificaciones>          Consultar(string rol);
        List<CalificacionesAuditoria> ConsultarAuditoria(string rol);
        Task<Calificaciones>          Guardar(Calificaciones entidad, string rol);
        Calificaciones                Modificar(Calificaciones entidad, string rol);
        Calificaciones                Eliminar(Calificaciones entidad, string rol);
    }
}
