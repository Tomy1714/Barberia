using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IAdministradoresPresentacion
    {
        List<Administradores>          Consultar(string rol);
        List<AdministradoresAuditoria> ConsultarAuditoria(string rol);
        Task<Administradores>          Guardar(Administradores entidad, string rol);
        Administradores                Modificar(Administradores entidad, string rol);
        Administradores                Eliminar(Administradores entidad, string rol);
    }
}
