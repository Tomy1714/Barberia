using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface ICombosPresentacion
    {
        List<Combos>          Consultar(string rol);
        List<CombosAuditoria> ConsultarAuditoria(string rol);
        Task<Combos>          Guardar(Combos entidad, string rol);
        Combos                Modificar(Combos entidad, string rol);
        Combos                Eliminar(Combos entidad, string rol);
    }
}
