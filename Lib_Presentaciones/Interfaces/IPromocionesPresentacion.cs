using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IPromocionesPresentacion
    {
        List<Promociones>          Consultar(string rol);
        List<PromocionesAuditoria> ConsultarAuditoria(string rol);
        Task<Promociones>          Guardar(Promociones entidad, string rol);
        Promociones                Modificar(Promociones entidad, string rol);
        Promociones                Eliminar(Promociones entidad, string rol);
    }
}
