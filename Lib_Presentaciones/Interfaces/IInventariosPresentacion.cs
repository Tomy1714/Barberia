using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IInventariosPresentacion
    {
        List<Inventarios>          Consultar(string rol);
        List<InventariosAuditoria> ConsultarAuditoria(string rol);
        Task<Inventarios>          Guardar(Inventarios entidad, string rol);
        Inventarios                Modificar(Inventarios entidad, string rol);
        Inventarios                Eliminar(Inventarios entidad, string rol);
    }
}
