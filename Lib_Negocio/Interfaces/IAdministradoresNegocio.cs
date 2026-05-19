using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IAdministradoresNegocio
    {
        void Configurar(string StringConexion);
        List<Administradores> Listar();
        Administradores? Guardar(Administradores? entidad);
        Administradores? Modificar(Administradores? entidad);
        Administradores? Borrar(Administradores? entidad);
        decimal CalcularSalario(Administradores entidad);
    }
}
