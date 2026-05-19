using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface ISedesNegocio
    {
        void Configurar(string StringConexion);
        List<Sedes> Listar();
        Sedes? Guardar(Sedes? entidad);
        Sedes? Modificar(Sedes? entidad);
        Sedes? Borrar(Sedes? entidad);
        bool HayPuestosDisponibles(Sedes entidad);
    }
}
