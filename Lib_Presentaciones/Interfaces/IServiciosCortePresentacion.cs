using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IServiciosCortePresentacion
    {
        List<ServiciosCorte>          Consultar(string rol);
        List<ServiciosCorteAuditoria> ConsultarAuditoria(string rol);
        Task<ServiciosCorte>          Guardar(ServiciosCorte entidad, string rol);
        ServiciosCorte                Modificar(ServiciosCorte entidad, string rol);
        ServiciosCorte                Eliminar(ServiciosCorte entidad, string rol);
    }
}
