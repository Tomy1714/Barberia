using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IEmpleadosPresentacion
    {
        List<Empleados>          Consultar(string rol);
        List<EmpleadosAuditoria> ConsultarAuditoria(string rol);
        Task<Empleados>          Guardar(Empleados entidad, string rol);
        Empleados                Modificar(Empleados entidad, string rol);
        Empleados                Eliminar(Empleados entidad, string rol);
    }
}
