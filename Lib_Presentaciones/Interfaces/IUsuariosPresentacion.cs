using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IUsuariosPresentacion
    {
        List<Usuarios>          Consultar(string rol);
        List<UsuariosAuditoria> ConsultarAuditoria(string rol);
        Task<Usuarios>          Guardar(Usuarios entidad, string rol);
        Usuarios                Modificar(Usuarios entidad, string rol);
        Usuarios                Eliminar(Usuarios entidad, string rol);
    }
}
