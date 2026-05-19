using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface INotificacionesNegocio
    {
        void Configurar(string StringConexion);
        List<Notificaciones> Listar();
        Notificaciones? Guardar(Notificaciones? entidad);
        Notificaciones? Modificar(Notificaciones? entidad);
        Notificaciones? Borrar(Notificaciones? entidad);
        bool Enviar(Notificaciones entidad);
    }
}
