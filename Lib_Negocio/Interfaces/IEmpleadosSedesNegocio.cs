using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IEmpleadosSedesNegocio
    {
        void Configurar(string StringConexion);
        List<EmpleadoSede> Listar();
        EmpleadoSede? Guardar(EmpleadoSede? entidad);
        EmpleadoSede? Modificar(EmpleadoSede? entidad);
        EmpleadoSede? Borrar(EmpleadoSede? entidad);
    }
}
