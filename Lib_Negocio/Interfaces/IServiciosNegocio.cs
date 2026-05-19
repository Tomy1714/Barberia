using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IServiciosNegocio
    {
        void Configurar(string StringConexion);
        List<Servicios> Listar();
        Servicios? Guardar(Servicios? entidad);
        Servicios? Modificar(Servicios? entidad);
        Servicios? Borrar(Servicios? entidad);
    }
}
