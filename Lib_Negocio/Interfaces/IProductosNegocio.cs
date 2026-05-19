using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IProductosNegocio
    {
        void Configurar(string StringConexion);
        List<Productos> Listar();
        Productos? Guardar(Productos? entidad);
        Productos? Modificar(Productos? entidad);
        Productos? Borrar(Productos? entidad);
        List<Productos> PorCategoria(string categoria);
    }
}
