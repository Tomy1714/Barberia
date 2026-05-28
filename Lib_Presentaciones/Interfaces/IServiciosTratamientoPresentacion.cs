using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IServiciosTratamientoPresentacion
    {
        List<ServiciosTratamiento>          Consultar(string rol);
        List<ServiciosTratamientoAuditoria> ConsultarAuditoria(string rol);
        Task<ServiciosTratamiento>          Guardar(ServiciosTratamiento entidad, string rol);
        ServiciosTratamiento                Modificar(ServiciosTratamiento entidad, string rol);
        ServiciosTratamiento                Eliminar(ServiciosTratamiento entidad, string rol);
    }
}
