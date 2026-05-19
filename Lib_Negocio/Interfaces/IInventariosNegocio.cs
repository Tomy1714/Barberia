using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IInventariosNegocio
    {
        void Configurar(string StringConexion);
        List<Inventarios> Listar();
        Inventarios? Guardar(Inventarios? entidad);
        Inventarios? Modificar(Inventarios? entidad);
        Inventarios? Borrar(Inventarios? entidad);
        int ContarProductosCriticos(Inventarios entidad);
    }
}
