using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IComboServiciosPresentacion
    {
        List<ComboServicios>          Consultar(string rol);
        List<ComboServiciosAuditoria> ConsultarAuditoria(string rol);
        Task<ComboServicios>          Guardar(ComboServicios entidad, string rol);
        ComboServicios                Modificar(ComboServicios entidad, string rol);
        ComboServicios                Eliminar(ComboServicios entidad, string rol);
    }
}
