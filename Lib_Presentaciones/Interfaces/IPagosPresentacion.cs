using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IPagosPresentacion
    {
        List<Pagos>          Consultar(string rol);
        List<PagosAuditoria> ConsultarAuditoria(string rol);
        Task<Pagos>          Guardar(Pagos entidad, string rol);
        Pagos                Modificar(Pagos entidad, string rol);
        Pagos                Eliminar(Pagos entidad, string rol);
    }
}
