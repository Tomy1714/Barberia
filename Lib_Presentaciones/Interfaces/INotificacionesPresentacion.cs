using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface INotificacionesPresentacion
    {
        List<Notificaciones>          Consultar(string rol);
        List<NotificacionesAuditoria> ConsultarAuditoria(string rol);
        Task<Notificaciones>          Guardar(Notificaciones entidad, string rol);
        Notificaciones                Modificar(Notificaciones entidad, string rol);
        Notificaciones                Eliminar(Notificaciones entidad, string rol);
    }
}
