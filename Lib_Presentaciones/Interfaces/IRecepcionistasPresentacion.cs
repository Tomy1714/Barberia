using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IRecepcionistasPresentacion
    {
        List<Recepcionistas>          Consultar(string rol);
        List<RecepcionistasAuditoria> ConsultarAuditoria(string rol);
        Task<Recepcionistas>          Guardar(Recepcionistas entidad, string rol);
        Recepcionistas                Modificar(Recepcionistas entidad, string rol);
        Recepcionistas                Eliminar(Recepcionistas entidad, string rol);
    }
}
