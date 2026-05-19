using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IEmpleadosNegocio
    {
        void Configurar(string StringConexion);
        List<Empleados> Listar();
        Empleados? Guardar(Empleados? entidad);
        Empleados? Modificar(Empleados? entidad);
        Empleados? Borrar(Empleados? entidad);
    }
}
