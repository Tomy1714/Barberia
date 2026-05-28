using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IInventarioProductosPresentacion
    {
        List<InventarioProductos>          Consultar(string rol);
        List<InventarioProductosAuditoria> ConsultarAuditoria(string rol);
        Task<InventarioProductos>          Guardar(InventarioProductos entidad, string rol);
        InventarioProductos                Modificar(InventarioProductos entidad, string rol);
        InventarioProductos                Eliminar(InventarioProductos entidad, string rol);
    }
}
