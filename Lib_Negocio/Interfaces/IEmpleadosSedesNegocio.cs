using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IEmpleadosSedesNegocio
    {
        void Configurar(string StringConexion);
        List<EmpleadosSedes> Listar();
        EmpleadosSedes? Guardar(EmpleadosSedes? entidad);
        EmpleadosSedes? Modificar(EmpleadosSedes? entidad);
        EmpleadosSedes? Borrar(EmpleadosSedes? entidad);
    }
}
