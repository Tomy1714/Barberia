using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IPuntosFidelidadPresentacion
    {
        List<PuntosFidelidad>          Consultar(string rol);
        List<PuntosFidelidadAuditoria> ConsultarAuditoria(string rol);
        Task<PuntosFidelidad>          Guardar(PuntosFidelidad entidad, string rol);
        PuntosFidelidad                Modificar(PuntosFidelidad entidad, string rol);
        PuntosFidelidad                Eliminar(PuntosFidelidad entidad, string rol);
    }
}
