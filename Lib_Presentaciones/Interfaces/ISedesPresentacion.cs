using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface ISedesPresentacion
    {
        List<Sedes>          Consultar(string rol);
        List<SedesAuditoria> ConsultarAuditoria(string rol);
        Task<Sedes>          Guardar(Sedes entidad, string rol);
        Sedes                Modificar(Sedes entidad, string rol);
        Sedes                Eliminar(Sedes entidad, string rol);
    }
}
