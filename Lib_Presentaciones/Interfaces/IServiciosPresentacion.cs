using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IServiciosPresentacion
    {
        List<Servicios>          Consultar(string rol);
        List<ServiciosAuditoria> ConsultarAuditoria(string rol);
        Task<Servicios>          Guardar(Servicios entidad, string rol);
        Servicios                Modificar(Servicios entidad, string rol);
        Servicios                Eliminar(Servicios entidad, string rol);
    }
}
