using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IHorariosDiasPresentacion
    {
        List<HorarioDias>          Consultar(string rol);
        List<HorariosDiasAuditoria> ConsultarAuditoria(string rol);
        Task<HorarioDias>          Guardar(HorarioDias entidad, string rol);
        HorarioDias                Modificar(HorarioDias entidad, string rol);
        HorarioDias                Eliminar(HorarioDias entidad, string rol);
    }
}
