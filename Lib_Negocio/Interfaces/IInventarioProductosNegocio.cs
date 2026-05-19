using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IInventarioProductosNegocio
    {
        void Configurar(string StringConexion);
        List<InventarioProductos> Listar();
        InventarioProductos? Guardar(InventarioProductos? entidad);
        InventarioProductos? Modificar(InventarioProductos? entidad);
        InventarioProductos? Borrar(InventarioProductos? entidad);
    }
}
