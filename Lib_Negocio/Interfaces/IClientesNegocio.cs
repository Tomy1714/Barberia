using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IClientesNegocio
    {
        void Configurar(string StringConexion);
        List<Clientes> Listar();
        Clientes? Guardar(Clientes? entidad);
        Clientes? Modificar(Clientes? entidad);
        Clientes? Borrar(Clientes? entidad);
        List<Clientes> PorVisitas(int minVisitas);
    }
}
