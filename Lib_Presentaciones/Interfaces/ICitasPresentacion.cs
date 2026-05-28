using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface ICitasPresentacion
    {
        List<Citas>          Consultar(string rol);
        List<CitasAuditoria> ConsultarAuditoria(string rol);
        Task<Citas>          Guardar(Citas entidad, string rol);
        Citas                Modificar(Citas entidad, string rol);
        Citas                Eliminar(Citas entidad, string rol);
    }
}
