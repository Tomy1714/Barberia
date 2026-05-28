using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IHorariosPresentacion
    {
        List<Horarios>          Consultar(string rol);
        List<HorariosAuditoria> ConsultarAuditoria(string rol);
        Task<Horarios>          Guardar(Horarios entidad, string rol);
        Horarios                Modificar(Horarios entidad, string rol);
        Horarios                Eliminar(Horarios entidad, string rol);
    }
}
