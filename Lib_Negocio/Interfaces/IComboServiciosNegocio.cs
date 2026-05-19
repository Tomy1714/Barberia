using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface IComboServiciosNegocio
    {
        void Configurar(string StringConexion);
        List<ComboServicios> Listar();
        ComboServicios? Guardar(ComboServicios? entidad);
        ComboServicios? Modificar(ComboServicios? entidad);
        ComboServicios? Borrar(ComboServicios? entidad);
    }
}
