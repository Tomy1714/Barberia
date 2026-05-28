using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IProductosPresentacion
    {
        List<Productos>          Consultar(string rol);
        List<ProductosAuditoria> ConsultarAuditoria(string rol);
        Task<Productos>          Guardar(Productos entidad, string rol);
        Productos                Modificar(Productos entidad, string rol);
        Productos                Eliminar(Productos entidad, string rol);
    }
}
