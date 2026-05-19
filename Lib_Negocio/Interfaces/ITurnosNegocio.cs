using Lib_Negocio.Entidades;

namespace Lib_Negocio.Interfaces
{
    public interface ITurnosNegocio
    {
        void Configurar(string StringConexion);
        List<Turnos> Listar();
        Turnos? Guardar(Turnos? entidad);
        Turnos? Modificar(Turnos? entidad);
        Turnos? Borrar(Turnos? entidad);
        List<Turnos> PorBarbero(int idBarbero);
    }
}
