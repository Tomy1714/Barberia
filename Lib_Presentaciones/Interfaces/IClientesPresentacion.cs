using Lib_Negocio.Entidades;

namespace LibPresentaciones.Interfaces
{
    public interface IClientesPresentacion
    {
        List<Clientes>          Consultar(string rol);
        List<ClientesAuditoria> ConsultarAuditoria(string rol);
        Task<Clientes>          Guardar(Clientes entidad, string rol);
        Clientes                Modificar(Clientes entidad, string rol);
        Clientes                Eliminar(Clientes entidad, string rol);
    }
}
