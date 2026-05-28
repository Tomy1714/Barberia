using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IPersonasPresentacion
    {
        List<Personas>          Consultar(string rol);
        List<PersonasAuditoria> ConsultarAuditoria(string rol);
        Task<Personas>          Guardar(Personas entidad, string rol);
        Personas                Modificar(Personas entidad, string rol);
        Personas                Eliminar(Personas entidad, string rol);
    }
}
