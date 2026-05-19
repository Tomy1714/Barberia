using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IPersonasNegocio
    {
        void Configurar(string StringConexion);
        List<Personas> Listar();
        Personas? Guardar(Personas? entidad);
        Personas? Modificar(Personas? entidad);
        Personas? Borrar(Personas? entidad);
    }
}
