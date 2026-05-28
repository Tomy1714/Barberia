using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface ITurnosPresentacion
    {
        List<Turnos>          Consultar(string rol);
        List<TurnosAuditoria> ConsultarAuditoria(string rol);
        Task<Turnos>          Guardar(Turnos entidad, string rol);
        Turnos                Modificar(Turnos entidad, string rol);
        Turnos                Eliminar(Turnos entidad, string rol);
    }
}
