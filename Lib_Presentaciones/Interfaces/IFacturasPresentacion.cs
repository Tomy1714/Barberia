using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IFacturasPresentacion
    {
        List<Facturas>          Consultar(string rol);
        List<FacturasAuditoria> ConsultarAuditoria(string rol);
        Task<Facturas>          Guardar(Facturas entidad, string rol);
        Facturas                Modificar(Facturas entidad, string rol);
        Facturas                Eliminar(Facturas entidad, string rol);
    }
}
