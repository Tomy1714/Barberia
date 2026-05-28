using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IEmpleadosSedesPresentacion
    {
        List<EmpleadoSede>          Consultar(string rol);
        List<EmpleadosSedesAuditoria> ConsultarAuditoria(string rol);
        Task<EmpleadoSede>          Guardar(EmpleadoSede entidad, string rol);
        EmpleadoSede                Modificar(EmpleadoSede entidad, string rol);
        EmpleadoSede                Eliminar(EmpleadoSede entidad, string rol);
    }
}
