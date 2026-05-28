using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IBarberosPresentacion
    {
        List<Barberos>          Consultar(string rol);
        List<BarberosAuditoria> ConsultarAuditoria(string rol);
        Task<Barberos>          Guardar(Barberos entidad, string rol);
        Barberos                Modificar(Barberos entidad, string rol);
        Barberos                Eliminar(Barberos entidad, string rol);
    }
}
